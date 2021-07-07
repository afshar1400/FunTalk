<template>
  <div class="container my-2">
    <div class="border-dark bg-light">
      <div class="container">
        <div class="row">
          <div class="col-2 d-flex align-items-center justify-content-end">
            <b-avatar
              variant="warning"
              text="BV"
              class=""
              size="3rem"
            ></b-avatar>
            {{ Joke.Created }}
          </div>
          <div class="col-10 py-3">
            <p class="text-left" style="font-size: 0.8rem !important">
              <router-link
                :to="{ name: 'detail', params: { id: Joke.id } }"
                class="text-secondary"
                style="text-decoration: none; curser: pointer"
              >
                {{ Joke.text }}
              </router-link>
            </p>
          </div>
        </div>

        <div class="d-flex justify-content-around py-2">
          <button class="btn btn-link" @click="likeJoke(Joke.id)">
            <span class="text-danger">{{ Joke.likeCount }}</span>
            <b-icon icon="heart-fill" font-scale="1" variant="warning"></b-icon>
          </button>
          <button class="btn btn-link" @click="showModal = !showModal">
            {{ Joke.cmtCount }}
            <b-icon icon="card-text" font-scale="1" variant="warning"></b-icon>
          </button>
          <button class="btn btn-link">
            <b-icon icon="upload" font-scale="1" variant="warning"></b-icon>
          </button>
        </div>
      </div>
    </div>

    <!-- model pops up for commenting -->
    <b-modal v-model="showModal" hide-footer title="what you think?">
      <div class="card">
        <div class="card-body">
          <p class="card-text">
            {{ Joke.text }}
          </p>
        </div>
      </div>
      <div class="my-3">
        <label>comment:</label>
        <b-form-input v-model="cmt" placeholder="Enter here"></b-form-input>
      </div>

      <div class="flex-left">
        <b-button class="mt-3" variant="warning" @click="submitCmt(Joke.id)"
          >send</b-button
        >
      </div>
    </b-modal>
  </div>
</template>

<script>
import myAxios from "../plugins/MyAxios";
export default {
  props: ["Joke"],
  data() {
    return {
      cmt: "",
      nameState: null,
      likeCount: this.$props.Joke.likeCount,
      showModal: false,
    };
  },
  methods: {
    async likeJoke(id) {
      var res = await myAxios.post("/api/Like/" + id);
      this.$props.Joke.likeCount = res.data;
      this.$store.commit("likeJoke", id);
    },
    async submitCmt(id) {
      let body = { text: this.cmt, JokeId: id };
      let res = await myAxios.post("/api/comment/", body);
      this.$props.Joke.cmtCount = res.data.cmtCount;
      this.showModal = false;
    },
  },
};
</script>

<style>
.flex-left {
  display: flex;
  flex-direction: row-reverse;
}
</style>
