<template>
  <div class="container">
    <Navbar />
    <div class="row">
      <div class="col-md-9">
        <router-view />
      </div>
      <div class="col-md-3">
        <div v-for="user in followUser" :key="user.userId">
          <follow-user-card :user="user"></follow-user-card>
        </div>
      </div>
    </div>
  </div>
</template>

<style>
textarea:focus,
textarea.form-control:focus,
input.form-control:focus,
input[type="text"]:focus,
input[type="password"]:focus,
input[type="email"]:focus,
input[type="number"]:focus,
[type="text"].form-control:focus,
[type="password"].form-control:focus,
[type="email"].form-control:focus,
[type="tel"].form-control:focus,
[contenteditable].form-control:focus {
  box-shadow: inset 0 -1px 0 #ddd;
}
a,
.btn:focus,
.btn:active:focus,
.btn.active:focus,
.btn.focus,
.btn:active.focus,
.btn.active.focus {
  box-shadow: none !important;
  border-color: #ffc107 !important;
}
.navbar {
  background: #ddd;
  border-bottom: 3px solid #eee;
}
.navbar-light .navbar-nav .nav-link {
  color: #2c3e50 !important;
}
</style>

<script>
import Navbar from "@/components/Navbar.vue";
import myAxios from "./plugins/MyAxios";
import FollowUserCard from "./components/FollowUserCard.vue";

export default {
  components: {
    Navbar,
    "follow-user-card": FollowUserCard,
  },
  data() {
    return {
      isAuthenticated: false,
      followUser: [],
    };
  },
  methods: {
    async getUserInfo() {
      var res = await myAxios.get("api/account/userInfo");
      var userInfo = res.data;
      console.log(res.data);
      if (res.status == 200) {
        await this.getListOfUserToFollow();
        this.$store.commit("addUser", userInfo.token, userInfo.username);
        this.$router.push("/");
      }
    },
    async getListOfUserToFollow() {
      let res = await myAxios.get("api/account/follow");
      this.followUser = res.data;
      console.log(res);
    },
  },
  async created() {
    await this.getUserInfo();
  },
};
</script>
