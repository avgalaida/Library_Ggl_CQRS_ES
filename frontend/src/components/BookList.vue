<template>
  <div>
    <transition-group name="book-list">
      <book-item
          v-for="book in this.$store.state.books"
          :key="book.createdAt"
          :book="book"
      />
    </transition-group>
    <h4 style="visibility: hidden">{{this.books[0]}}</h4>
  </div>
</template>

<script>
import gql from 'graphql-tag'
import bookItem from "@/components/Book"

export default {
  apollo: {
    books: gql`
      query {
        books {
          id
          status
          title
          authors
          createdAt
          lastRevision
        }
      }
    `,
    // $subscribe: {
    //   bookCreated: {
    //     query: gql`
    //       subscription {
    //         onBookCreated {
    //           id
    //           status
    //           title
    //           authors
    //           createdAt
    //           lastRevision
    //         }
    //       }
    //     `,
    //     // reuslt ({data}) {
    //     //   data.bookCreated
    //     // }
    //   }
    // }
  },
  updated() {
    this.$store.state.books = this.books;
  },
  components: {bookItem},
}

</script>

<style scoped>
</style>