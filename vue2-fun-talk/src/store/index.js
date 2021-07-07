import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

const store = new Vuex.Store({
  state: {
    token: null,
    isAuthenticated: false,
    timeline: null,
    explorer: null,
    username: null,
  },
  mutations: {
    addToken(state, bearertoken) {
      state.isAuthenticated = true;
      state.token = bearertoken;
    },
    addUser(state, token, username) {
      state.isAuthenticated = true;
      state.token = token;
      state.username = username;
      localStorage.setItem("funtalk", token);
    },
    signout(state) {
      localStorage.removeItem("funtalk");
      state.isAuthenticated = false;
      state.token = null;
    },
    addTimeline(state, line) {
      //timeline with pageing
      state.timeline = line;
    },
    addExplorer(state, explorer) {
      //explorer with paging add
      state.explorer = explorer;
    },
    likeJoke(state, id) {
      //for likeing joke in
      state.timeline = state.timeline.items.map((x) => {
        if (x.id == id) {
          x.likeCount++;
        } else {
          return x;
        }
        state.timeline = state.explorer.items.map((x) => {
          if (x.id == id) {
            x.likeCount++;
          } else {
            return x;
          }
        });
      });
    },
  },
});

export default store;
