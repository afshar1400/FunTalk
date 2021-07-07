<template>
  <div class="my-3">
    <loader :loading="loading">
      <joke-card :Joke="detail"></joke-card>

      <div class="container m-1">
        <div v-for="cmt in detail.mainCmts" :key="cmt.id">
          <b-media class="my-1 p-2">
            <template #aside>
              <b-img
                blank
                blank-color="#abc"
                width="32"
                alt="placeholder"
              ></b-img>
            </template>
            <p class="mb-1">
              {{ cmt.comment }}
            </p>
          </b-media>
        </div>
      </div>
    </loader>
  </div>
</template>

<script>
import JokeCard from "../components/JokeCard.vue";
import Loader from "../components/Loader.vue";
import myAxios from "../plugins/MyAxios";
export default {
  components: { JokeCard, Loader },
  data() {
    return {
      detail: null,
      loading: true,
    };
  },
  async created() {
    let id = this.$route.params.id;
    let res = await myAxios.get("/api/joke/" + id);
    this.detail = res.data;
    this.loading = false;
  },
};
</script>

<style></style>
