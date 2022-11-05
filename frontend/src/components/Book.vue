<template>
  <div>
    <div><strong>Название:</strong> {{title}}</div>
    <div><strong>Авторы:</strong> {{authors}}</div>
    <div><strong>Ревизия:</strong> {{revision}}</div>
  </div>
</template>

<script>
export default {
  props: ['book'],
  computed: {
    status() {return this.book.status},
    title() {return this.book.title},
    authors() {return this.book.authors},
    revision() {return this.book.revision},
  },
}
</script>

<style scoped>

</style>