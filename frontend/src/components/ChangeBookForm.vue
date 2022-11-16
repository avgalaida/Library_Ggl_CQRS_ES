<template>
  <form @submit.prevent>
    <h3 style="color: darkslategrey">Изменение книги</h3>
    <my-input placeholder="Название" v-model="newBookTitle" type="text"></my-input>
    <my-input placeholder="Авторы" v-model="newBookAuthors" type="text"></my-input>
    <button class="createBtn" type="button" @click="changeBook" style="margin-top: 20px;">Изменить</button>
  </form>
</template>

<script>
import myInput from "@/components/UI/MyInput";
import gql from "graphql-tag";

export default {
  data() {
    return {newBookTitle: this.book.title, newBookAuthors: this.book.authors}
  },
  props: ['book'],
  computed:{
    bookTitle() {return this.book.title},
    bookAuthors() {return this.book.authors},
    id() {return this.book.id}
  },
  methods: {
    async changeBook(){
      if (this.bookTitle !== this.newBookTitle) {
        await this.$apollo.mutate({
          mutation: gql`
          mutation ($eventData: ChangeBookTitleEventInput!) {
            changeBookTitle(request: $eventData) {
              id
            }
          }
        `,
          variables: {
            eventData: {
              aggregateId: this.id,
              title: this.newBookTitle
            }
          }
        });
      }
      if (this.bookAuthors !== this.newBookAuthors) {
        await this.$apollo.mutate({
          mutation: gql`
          mutation ($eventData: ChangeBookAuthorsEventInput!) {
            changeBookAuthors(request: $eventData) {
              id
            }
          }
        `,
          variables: {
            eventData: {
              aggregateId: this.id,
              authors: this.newBookAuthors
            }
          }
        });
      }
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