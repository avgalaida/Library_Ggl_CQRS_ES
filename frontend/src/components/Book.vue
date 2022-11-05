<template>
  <div class="book">
    <div>
      <div><strong>Название:</strong> {{title}}</div>
      <div><strong>Авторы:</strong> {{authors}}</div>
      <div><strong>Ревизия:</strong> {{revision}}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: "book-item",
  props: ['book'],
  computed: {
    status() {return this.book.status},
    title() {return this.book.title},
    authors() {return this.book.authors},
    revision() {return this.book.lastRevision},
  },
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