<template>
  <div>
    <loader-page :loading="loading">
      <joke-card v-for="item in explorer.items" :key="item.id" :Joke="item" />
    </loader-page>
  </div>
</template>

<script>
import JokeCard from "../components/JokeCard.vue";
import Loader from "../components/Loader.vue";
import MyAxios from "../plugins/MyAxios.js";
export default {
  components: {
    "joke-card": JokeCard,
    "loader-page": Loader,
  },
  data() {
    return {
      loading: false,
      explorer: null,
    };
  },
  methods: {
    async getExplorer() {
      let res = await MyAxios.get("/api/Joke/Explorer");
      let explorer = res.data;
      this.explorer = explorer;
      this.$store.commit("addExplorer", explorer);
    },
  },
  async created() {
    await this.getExplorer();
    this.loading = false;
  },
};
</script>
