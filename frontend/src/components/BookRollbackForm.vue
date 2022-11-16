<template >
  <div @submit.prevent>
    <h3 style="color: darkslategrey; text-align: center">Откат книги</h3>
    <form class="rollback">
      <div>
        <div><strong>Название:</strong>  {{bookWithRevision[0].title}}</div>
        <div><strong>Авторы:</strong> {{bookWithRevision[0].authors}}</div>
        <div><strong>Статус:</strong> {{bookWithRevision[0].status}}</div>
      </div>
      <div>
        <select v-model="selected" @change="onChange($event)" style="margin-left: 20px;font-family: Georgia, serif;color: darkslategrey;" >
          <option disabled value="">Ревизия</option>
          <option v-for="option in options" :key="option.value" v-bind:selected="option.value">
            {{option}}
          </option>
        </select>
        <button class="createBtn" type="button" @click="rollbackBook" style="margin-left: 20px;">Изменить</button>
      </div>
    </form>
  </div>
</template>

<script>
import gql from "graphql-tag";

export default {
  data() {
    return {
      numeric: this.book.lastRevision,
      bookWithRevision: [{title:"",authors:"",status:""}],
      selected: this.book.lastRevision,
      options: Array.from({length: this.book.lastRevision - 1}, (_, i) => this.book.lastRevision - i - 1)
    }
  },
  apollo: {
    ping() {
      return {
        query: gql`
          query($id: String!, $revision: Int!) {
            bookWithRevision(id: $id, revision: $revision) {
              status
              title
              authors
            }
          }
        `,
        variables() {
          return {
            id: this.book.id,
            revision: this.numeric
          }
        },
        update: function(data) {  
          this.bookWithRevision[0].title = data.bookWithRevision.title
          this.bookWithRevision[0].authors = data.bookWithRevision.authors
          this.bookWithRevision[0].status = data.bookWithRevision.status
        }
      }
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
  },
  methods: {
     onChange() {
       this.numeric = 0;
       while (this.numeric != this.selected && this.numeric < 10) {
         this.numeric++
       }
    },
    async rollbackBook() {
      await this.$apollo.mutate({
        mutation: gql`
          mutation ($delta: RollbackBookEventInput!) {
            rollbackBook(request: $delta) {
              id
            }
          }
        `,
        variables: {
          delta: {
            aggregateId: this.book.id,
            status: this.bookWithRevision[0].status,
            title:  this.bookWithRevision[0].title ,
            authors: this.bookWithRevision[0].authors
          }
        }
      });
    }
      // this.$store.dispatch('rollbackBook', {
      //   id: this.id,
      //   title: this.newTitle,
      //   authors: this.newAuthors,
      //   status: this.newStatus
      // });
  },
  mounted() {
    this.bookWithRevision.at(0).title = this.book.title
    this.bookWithRevision.at(0).authors = this.book.authors
    this.bookWithRevision.at(0).status = this.book.status
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