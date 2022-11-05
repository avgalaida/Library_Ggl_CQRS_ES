<template >
  <div @submit.prevent>
    <h3 style="color: darkslategrey; text-align: center">Откат книги</h3>
    <form class="rollback">
      <div>
        <div><strong>Название:</strong>  {{newTitle}}</div>
        <div><strong>Авторы:</strong> {{newAuthors}}</div>
        <div><strong>Статус:</strong> {{newStatus}}</div>
      </div>
      <div>
        <select v-model="selected" @change="onChange($event)" style="margin-left: 20px;font-family: Georgia, serif;color: darkslategrey;" >
          <option disabled value="">Ревизия</option>
<!--          <option v-for="option in options" v-bind:selected="option.value">-->
<!--            {{option}}-->
<!--          </option>-->
        </select>
        <button class="createBtn" type="button" @click="rollbackBook" style="margin-left: 20px;">Изменить</button>
      </div>
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      getVersionResult: [{title:"",authors:"",status:""}],
      selected: '',
      options: Array.from({length: this.book.lastRevision - 1}, (_, i) => this.book.lastRevision - i - 1)
    }
  },
  props: ['book'],
  computed: {
    id() {return this.book.id},
    lastRevision() {return this.book.lastRevision},
    status() {return this.book.status},
    title() {return this.book.title},
    authors() {return this.book.authors},
    createdAt() {return this.book.createdAt},
    newTitle() {return this.getVersionResult.at(0).title},
    newAuthors() {return this.getVersionResult.at(0).authors},
    newStatus() {return this.getVersionResult.at(0).status},
  },
  methods: {
    onChange() {
      // this.$store.dispatch('getVersion', {
      //   id: this.id,
      //   version: this.selected,
      // });
    },
    rollbackBook() {
      // this.$store.dispatch('rollbackBook', {
      //   id: this.id,
      //   title: this.newTitle,
      //   authors: this.newAuthors,
      //   status: this.newStatus
      // });
    },
  },
  mounted() {
    this.getVersionResult.at(0).title = this.book.title
    this.getVersionResult.at(0).authors = this.book.authors
    this.getVersionResult.at(0).status = this.book.status
  }
}

</script>

<style scoped>
.rollback {
  color: darkslategrey;
  display: flex;
  /*align-items: start;*/
}
</style>