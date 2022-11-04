<template>
  <div>
    <h1>"qwe"</h1>
    <div v-for="book in allBook" :key="book.id" >
      {{book.title}}
    </div>
  </div>
</template>

<script>
import gql from 'graphql-tag'

export default {
  apollo: {
    allBook: gql`
    query {
      allBook {
        id
        title
        authors
        status
      }
    }`,
  }
}
</script>

<style scoped>

</style>