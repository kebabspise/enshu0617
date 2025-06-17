<template>
  <div>
    <router-link to="/admin" v-if="route.path !== '/admin'">
      <button>管理画面へ</button>
    </router-link>

    <h1>会議室一覧</h1>

    <ul>
      <li v-for="room in rooms" :key="room.id">
        {{ room.name }} - {{ room.size }}人　
        <span v-if="room.book">【予約中】</span>
        <span v-else>【空き】</span>

        <button v-if="!room.book" @click="reserveRoom(room)">予約</button>
        <button v-else @click="cancelRoom(room)">キャンセル</button>
      </li>
    </ul>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import axios from 'axios'

const rooms = ref([])
const route = useRoute()

const fetchRooms = async () => {
  try {
    const res = await axios.get('http://localhost:5220/api/rooms')
    rooms.value = res.data 
  } catch (error) {
    console.error("データ取得エラー:", error)
  }
}

onMounted(fetchRooms)

// 予約処理（Book = true）
const reserveRoom = async (room) => {
  try {
    await axios.put(`http://localhost:5220/api/rooms/${room.id}`, {
      ...room,
      book: true
    })
    fetchRooms()
  } catch (error) {
    console.error("予約エラー:", error)
  }
}

// キャンセル処理（Book = false）
const cancelRoom = async (room) => {
  try {
    await axios.put(`http://localhost:5220/api/rooms/${room.id}`, {
      ...room,
      book: false
    })
    fetchRooms()
  } catch (error) {
    console.error("キャンセルエラー:", error)
  }
}
</script>