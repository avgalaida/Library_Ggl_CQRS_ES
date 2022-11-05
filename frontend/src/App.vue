<template>
  <div class="container py-5">
    <div class="row mb-4">
      <h1 class="col-12" style="text-align: center">БИБЛИОТЕКА</h1>
    </div>
    <button class="createBtn" type="button" @click="showCreateDialog" style="font-size: 1rem;">Создать книгу</button>
    <h4 style="padding: 20px 0; text-align: center;" >Список книг</h4>
    <my-dialog v-model="createDialogVisible"><create-book-form/></my-dialog>
    <book-list  class="col"/>
  </div>
</template>

<script>
import bookList from "@/components/BookList";
import myDialog from "@/components/UI/MyDialog";
import createBookForm from "@/components/CreateBookForm";

export default {
  data(){
    return {createDialogVisible: false}
  },
  methods: {
    showCreateDialog() {this.createDialogVisible = true},
  },
  components: {
    bookList,
    myDialog,
    createBookForm, 
  },

}
</script>

<style>
.container {
  color: darkslategrey;
  font-family: Georgia;

  max-width: 900px;
}
.createBtn{
  background: mediumseagreen;
  color: white;
}
/*.bookBtns{*/
/*  background: white;*/
/*  color: mediumseagreen;*/
/*}*/
button{
  background: white;
  color: mediumseagreen;
  transition: background 0.25s ease-in;
  border-radius: 4px;
  border: none;
  padding: 8px 16px;
  font-size: 1rem;
  cursor: pointer;
  text-align: center;
}
button:hover,
button:focus {
  transition: background 0.3s ease-out;
  background: darkslategrey;
  color: white;
}
button:focus {
  outline: 1px solid #fff;
  outline-offset: -4px;
}
</style>
