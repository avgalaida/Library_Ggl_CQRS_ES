<template>
  <form @submit.prevent>
    <h3>Создание книги</h3>
    <my-input placeholder="Название" v-model="bookTitle" type="text"></my-input>
    <my-input placeholder="Авторы" v-model="bookAuthors" type="text"></my-input>
    <button class="createBtn" type="button" @click="createBook" style="margin-top: 20px;">Создать</button>
  </form>
</template>

<script>
import gql from 'graphql-tag'
import myInput from "@/components/UI/MyInput";

export default {
  data() {
    return {bookTitle: '', bookAuthors: ''}
  },
  methods: {
    async createBook() {
      await this.$apollo.mutate({
        mutation: gql`
          mutation ($delta: CreateBookEventInput!) {
            createBook(bookEvent: $delta) {
              id
            }
          }
        `,
        variables: {
          delta: {
            status: "Доступна",
            title: this.bookTitle,
            authors: this.bookAuthors
          }
        }
      });
      this.bookTitle = ''; this.bookAuthors= '';
    }
  },
  components: {myInput}
}
</script>

<style scoped>
form {
  display: flex;
  flex-direction: column;
}
</style>