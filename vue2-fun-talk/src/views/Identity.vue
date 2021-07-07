<template>
  <b-container>
    <b-row align="center" class="mt-4 p-1">
      <b-col md="12" lg="8" xl="6" class="my-auto mx-auto">
        <b-tabs
          content-class="mt-3"
          align="center"
          active-nav-item-class="font-weight-bold text-uppercase text-warning"
          active-tab-class="font-weight-bold text-dark"
        >
          <b-tab title="Resgister">
            <b-form>
              <b-form-group
                id="input-group-1"
                label="Email address:"
                label-for="input-1"
                description="We'll never share your email with anyone else."
              >
                <b-form-input
                  id="#input-1"
                  v-model="form.email"
                  type="email"
                  placeholder="Enter email"
                  required
                ></b-form-input>
              </b-form-group>

              <b-form-group
                id="input-group-2"
                label="Your username:"
                label-for="input-2"
              >
                <b-form-input
                  id="#input-2"
                  v-model="form.username"
                  placeholder="Enter name"
                  required
                ></b-form-input>
              </b-form-group>

              <b-form-group
                id="input-group-3"
                label="Password:"
                label-for="input-3"
              >
                <b-form-input
                  id="#input-3"
                  v-model="form.password"
                  required
                ></b-form-input>
              </b-form-group>

              <button
                type="submit"
                class="btn btn-warning btn-block"
                @click="registerSubmit"
              >
                Submit
              </button>
            </b-form>
          </b-tab>
          <b-tab title="login" active>
            <b-form @submit.prevent="loginSubmit">
              <b-form-group
                id="input-group-4"
                label="Email address:"
                label-for="#input-4"
                description="We'll never share your email with anyone else."
              >
                <b-form-input
                  id="input-4"
                  v-model="form.email"
                  type="email"
                  placeholder="Enter email"
                  required
                ></b-form-input>
              </b-form-group>

              <b-form-group
                id="input-group-5"
                label="Password:"
                label-for="#input-5"
              >
                <b-form-input
                  id="#input-5"
                  v-model="form.password"
                  required
                ></b-form-input>
              </b-form-group>

              <b-button type="submit" class="btn-block" variant="warning"
                >Submit</b-button
              >
            </b-form>
          </b-tab>
        </b-tabs>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      form: { email: "", username: "", password: "" },
    };
  },
  methods: {
    async registerSubmit() {
      var res = await axios.post("/api/Account/register", this.form);
      var IsRegisted = res.data;
      console.log(IsRegisted);
    },

    async loginSubmit() {
      let loginform = { email: this.form.email, password: this.form.password };
      var res = await axios.post("/api/Account/login", loginform);
      var token = res.data;
      if (token != null) {
        localStorage.setItem("funtalk", token);
        this.$store.commit("addToken", token);
      }
      console.log(token);
      this.$router.push("/");
    },
  },
};
</script>

<style></style>
