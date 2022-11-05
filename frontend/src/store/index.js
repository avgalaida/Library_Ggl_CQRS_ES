import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    books: []
  },
  getters: {
    set_books(state,books) {
      state.books = books;
    },
    create_book(state,book) {
      state.books = [book, ...state.books]
    }
  },
  mutations: {
  },
  actions: {
  },
  modules: {
  }
})
