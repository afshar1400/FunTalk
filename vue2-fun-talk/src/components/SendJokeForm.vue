<template>
  <b-container class="home">
    <div class="write-joke bg-light my-2">
      <b-form @submit.prevent="SendJoke" class="mb-0">
        <div class="row">
          <div class="col-2 d-flex align-items-center justify-content-end">
            <b-avatar
              variant="warning"
              text="BV"
              class=""
              size="3rem"
            ></b-avatar>
          </div>
          <div class="col-10">
            <b-form-textarea
              id="input-8"
              v-model="text"
              placeholder="one day"
              required
              rows="3"
              class="border-0 w-100 bg-light"
            ></b-form-textarea>
          </div>
        </div>

        <div class="row mt-0">
          <div class="col-md-10"></div>
          <div class="col-md-2">
            <b-button
              type="submit"
              variant="outline-link"
              class="w-100 border-0"
              >send</b-button
            >
          </div>
        </div>
      </b-form>
    </div>
  </b-container>
</template>

<script>
import myAxios from "../plugins/MyAxios";
export default {
  data() {
    return {
      text: null,
    };
  },
  methods: {
    async SendJoke() {
      var form = { text: this.text };
      let res = await myAxios.post("/api/Joke", form);
      this.text = "";
      console.log(res);
    },
  },
};
</script>

<style scoped>
.write-joke {
  border: 1px solid rgba(221, 221, 221, 0.596);
}
</style>
