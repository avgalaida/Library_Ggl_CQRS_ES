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
              id
            }
          }
        `,
        result (data) {
          let i = this.findIndex(data.data.onBookDeleted.id)
          this.books.at(i).status = "Недоступна";
          this.books.at(i).lastRevision++;
        }
      },
      onBookRestored: {
        query: gql`
          subscription {
            onBookRestored {
              id
              status
            }
          }
        `,
        result (data) {
          let i = this.findIndex(data.data.onBookRestored.id)
          this.books.at(i).status = data.data.onBookRestored.status;
          this.books.at(i).lastRevision++;
        }
      },
      onBookTitleChanged: {
        query: gql`
          subscription {
            onBookTitleChanged {
              id
              title
            }
          }
        `,
        result (data) {
          let i = this.findIndex(data.data.onBookTitleChanged.id)
          this.books.at(i).title = data.data.onBookTitleChanged.title;
          this.books.at(i).lastRevision++;
        }
      },
      onBookAuthorsChanged: {
        query: gql`
          subscription {
            onBookAuthorsChanged {
              id
              authors
            }
          }
        `,
        result (data) {
          let i = this.findIndex(data.data.onBookAuthorsChanged.id)
          this.books.at(i).authors = data.data.onBookAuthorsChanged.authors;
          this.books.at(i).lastRevision++;
        }
      },
      onBookRollbacked: {
        query: gql`
          subscription {
            onBookRollbacked {
              id
              status
              title
              authors
            }
          }
        `,
        result (data) {
          let i = this.findIndex(data.data.onBookRollbacked.id)
          this.books.at(i).status = data.data.onBookRollbacked.status;
          this.books.at(i).title = data.data.onBookRollbacked.title;
          this.books.at(i).authors = data.data.onBookRollbacked.authors;
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
</script>


<style scoped>
</style>