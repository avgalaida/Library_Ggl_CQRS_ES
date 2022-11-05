<template>
  <div>
    <transition-group name="book-list">
      <book-item
          v-for="book in this.books"
          :key="book.createdAt"
          :book="book"
      />
    </transition-group>
  </div>
</template>

<script>
import gql from 'graphql-tag';
import bookItem from "@/components/Book";

export default {
  data() {
    return {books: []}
  },
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
    $subscribe: {
      onBookCreated: {
        query: gql`
          subscription {
            onBookCreated {
              id
              status
              title
              authors
              createdAt
              lastRevision
            }
          }
        `,
        result (data) {
          // console.log(data)
          this.books = [data.data.onBookCreated, ...this.books]
        }
      },
      onBookDeleted: {
        query: gql`
          subscription {
            onBookDeleted {
              aggregateId
            }
          }
        `,
        result (data) {
          let i = this.findIndex(data.data.onBookDeleted.aggregateId)
          this.books.at(i).status = "Недоступна";
          this.books.at(i).lastRevision++;
        }
      },
      onBookRestored: {
        query: gql`
          subscription {
            onBookRestored {
              aggregateId
              status
            }
          }
        `,
        result (data) {
          let i = this.findIndex(data.data.onBookDeleted.aggregateId)
          this.books.at(i).status = data.data.onBookDeleted.status;
          this.books.at(i).lastRevision++;
        }
      },
      onBookTitleChanged: {
        query: gql`
          subscription {
            onBookTitleChanged {
              aggregateId
              title
            }
          }
        `,
        result (data) {
          let i = this.findIndex(data.data.onBookDeleted.aggregateId)
          this.books.at(i).title = data.data.onBookDeleted.title;
          this.books.at(i).lastRevision++;
        }
      },
      onBookAuthorsChanged: {
        query: gql`
          subscription {
            onBookAuthorsChanged {
              aggregateId
              authors
            }
          }
        `,
        result (data) {
          let i = this.findIndex(data.data.onBookDeleted.aggregateId)
          this.books.at(i).authors = data.data.onBookDeleted.authors;
          this.books.at(i).lastRevision++;
        }
      },
      onBookRollbacked: {
        query: gql`
          subscription {
            onBookRollbacked {
              aggregateId
              status
              title
              authors
            }
          }
        `,
        result (data) {
          let i = this.findIndex(data.data.onBookDeleted.aggregateId)
          this.books.at(i).status = data.data.onBookDeleted.status;
          this.books.at(i).title = data.data.onBookDeleted.title;
          this.books.at(i).authors = data.data.onBookDeleted.authors;
          this.books.at(i).lastRevision++;
        }
      }
    }
  },
  methods: {
    findIndex(id) {
      let index; let i;
      for (i = 0;i < this.books.length; ++i) {
        if (this.books.at(i).id === id) {
          index = i;
          break;
        }
      }
      return index;
    }
  },
  components: {bookItem},
}
// function findIndex(id) {
//   let index; let i;
//   for (i = 0;i < this.books.length; ++i) {
//     if (this.books.at(i).id === id) {
//       index = i;
//       break;
//     }
//   }
//   return index;
// }
</script>


<style scoped>
</style>