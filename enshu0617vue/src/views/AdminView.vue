<template>
  <div>
    <!-- ホームに戻るボタン -->
    <router-link to="/">
      <button>予約画面へ</button>
    </router-link>

    <h1>会議室管理</h1>

    <!-- 会議室一覧 -->
    <ul>
      <li v-for="room in rooms" :key="room.id">
        {{ room.name }}（定員: {{ room.size }}人）
        <button @click="editRoom(room)">編集</button>
        <button @click="deleteRoom(room.id)">削除</button>
      </li>
    </ul>

    <!-- 新規/編集フォーム -->
    <h2>{{ isEditing ? '会議室を編集' : '新しい会議室を追加' }}</h2>
    <form @submit.prevent="isEditing ? updateRoom() : addRoom()">
      <div>
        <label>名前:</label>
        <input v-model="form.name" required />
      </div>
      <div>
        <label>定員:</label>
        <input v-model.number="form.size" type="number" min="1" required />
      </div>
      <button type="submit">{{ isEditing ? '更新' : '追加' }}</button>
      <button type="button" v-if="isEditing" @click="cancelEdit">キャンセル</button>
    </form>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'

const rooms = ref([])

// APIベースURL（必要に応じてポートを変更）
const baseUrl = 'http://localhost:5220/api/rooms'

// フォームの状態
const form = ref({ id: null, name: '', size: 1 })
const isEditing = ref(false)

// 会議室一覧取得
const fetchRooms = async () => {
  try {
    const res = await axios.get(baseUrl)
    rooms.value = res.data
  } catch (err) {
    console.error('取得エラー:', err)
  }
}

// 追加
const addRoom = async () => {
  try {
    await axios.post(baseUrl, { name: form.value.name, size: form.value.size })
    form.value = { id: null, name: '', size: 1 }
    fetchRooms()
  } catch (err) {
    console.error('追加エラー:', err)
  }
}

// 削除
const deleteRoom = async (id) => {
  if (!confirm('本当に削除しますか？')) return
  try {
    await axios.delete(`${baseUrl}/${id}`)
    fetchRooms()
  } catch (err) {
    console.error('削除エラー:', err)
  }
}

// 編集準備
const editRoom = (room) => {
  form.value = { ...room }
  isEditing.value = true
}

// 更新
const updateRoom = async () => {
  try {
    await axios.put(`${baseUrl}/${form.value.id}`, {
      name: form.value.name,
      size: form.value.size
    })
    form.value = { id: null, name: '', size: 1 }
    isEditing.value = false
    fetchRooms()
  } catch (err) {
    console.error('更新エラー:', err)
  }
}

// 編集キャンセル
const cancelEdit = () => {
  form.value = { id: null, name: '', size: 1 }
  isEditing.value = false
}

// 初回読み込み
onMounted(fetchRooms)
</script>

<style scoped>
h1, h2 {
  margin-bottom: 0.5em;
}
form {
  margin-top: 1em;
  padding: 1em;
  border: 1px solid #ccc;
  display: inline-block;
}
form div {
  margin-bottom: 0.5em;
}
button {
  margin-right: 0.5em;
}
</style>