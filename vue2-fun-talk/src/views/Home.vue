<template>
  <div>
    <SendJokeForm />
    <loader-page :loading="loading">
      <JokeCard v-for="joke in timeline.items" :key="joke.id" :Joke="joke" />
    </loader-page>
  </div>
</template>

<script>
// @ is an alias to /src
//import HelloWorld from "@/components/HelloWorld.vue";
import SendJokeForm from "../components/SendJokeForm.vue";
import JokeCard from "../components/JokeCard.vue";
import myAxios from "../plugins/MyAxios.js";
import Loader from "../components/Loader.vue";
export default {
  name: "Home",
  components: {
    SendJokeForm,
    JokeCard,
    "loader-page": Loader,
  },
  data() {
    return {
      loading: true,
      timeline: {
        items: [
          {
            id: 1,
            text: "one day sth eat nothing",
          },
        ],
      },
    };
  },
  methods: {
    async getTimeline() {
      let res = await myAxios.get("/api/Joke/Timeline");
      this.timeline = res.data;
      this.$store.commit("addTimeline", res.data);
    },
  },
  computed: {},
  async created() {
    await this.getTimeline();
    this.loading = false;
  },
};
</script>

<style scoped></style>
