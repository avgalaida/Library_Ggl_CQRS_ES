<template>
  <div id="app">
    <img alt="Vue logo" src="./assets/logo.png">
    <test-graphql/>
  </div>
</template>

<script>
import TestGraphql from './components/TestGraphql'

export default {
  name: 'App',
  components: {
    TestGraphql
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
