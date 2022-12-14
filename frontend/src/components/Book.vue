<template>
  <div v-if="status !== 'Недоступна'" class="book" @submit.prevent>
    <div>
      <div><strong>Название:</strong> {{title}}</div>
      <div><strong>Авторы:</strong> {{authors}}</div>
      <div><strong>Ревизия:</strong> {{lastRevision}}</div>
    </div>
    <my-dialog v-model="changeDialogVisible"><change-book-form :book="this.book"/></my-dialog>
    <my-dialog v-model="bookRollbackDialogVisible"><book-rollback-form :book="this.book"/></my-dialog>
    <div>
      <button @click="showRollbackDialog" type="button" style="margin-right: 10px;">Откат</button>
      <button @click="showChangeDialog" type="button" style="margin-right: 10px;">Изменить</button>
      <button @click="deleteBook">Удалить</button>
    </div>
  </div>

  <div v-else class="deletedBook" @submit.prevent>
    <div>
      <div><strong>Название:</strong> {{title}}</div>
      <div><strong>Авторы:</strong> {{authors}}</div>
      <div><strong>Ревизия:</strong> {{lastRevision}}</div>
    </div>
    <my-dialog v-model="bookRollbackDialogVisible"><book-rollback-form :book="this.book"/></my-dialog>
    <div class="book__btn">
      <button class="restoreBtn" @click="showRollbackDialog" style="margin-right: 10px;">Откат</button>
      <button class="restoreBtn" @click="restoreBook">Восстановить</button>
    </div>
  </div>
</template>

<script>
import changeBookForm from "@/components/ChangeBookForm";
import bookRollbackForm from "@/components/BookRollbackForm";
import myDialog from "@/components/UI/MyDialog";
import gql from "graphql-tag";
export default {
  name: "book-item",
  props: ['book'],
  data(){
    return {changeDialogVisible: false, bookRollbackDialogVisible: false }
  },
  computed: {
    id() {return this.book.id},
    status() {return this.book.status},
    title() {return this.book.title},
    authors() {return this.book.authors},
    lastRevision() {return this.book.lastRevision},
    createdAt() {return this.book.createdAt}
  },
  methods:{
    async deleteBook(){
      await this.$apollo.mutate({
        mutation: gql`
          mutation ($id: String!) {
            deleteBook(id: $id) {
              id
            }
          }
        `,
        variables: {
          id: this.id
        }
      });
    },
    async restoreBook(){
      await this.$apollo.mutate({
        mutation: gql`
          mutation ($eventData: RestoreBookEventInput!) {
            restoreBook(request: $eventData) {
              id
            }
          }
        `,
        variables: {
          eventData: {
            aggregateId: this.id,
            status: "Доступна"
          }
        }
      });
    },
    showChangeDialog(){this.changeDialogVisible = true;},
    showRollbackDialog(){this.bookRollbackDialogVisible = true;}
  },
  components: {changeBookForm, bookRollbackForm, myDialog}
}
</script>

<style scoped>
.restoreBtn{
  color: darkslategrey;
  border: white;
}
.restoreBtn:hover,
.restoreBtn:focus {
  transition: background 0.3s ease-out;
  background: mediumseagreen;
  color: white;
}
.restoreBtn:focus {
  outline: 1px solid #fff;
  outline-offset: -4px;
}
.book{
  background: mediumseagreen;
  color: white;
  padding: 15px;
  border: 1px solid mediumseagreen;
  margin-top: 15px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  border-radius: 4px;
}
.deletedBook{
  background: darkslategrey;
  color: white;
  padding: 15px;
  border: 1px solid gray;
  margin-top: 15px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  border-radius: 4px;
}
</style>