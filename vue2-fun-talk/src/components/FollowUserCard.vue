<template>
  <div class="card">
    <div class="card-body">
      <div class="row">
        <div class="col-6">{{ user.username }}</div>
        <div class="col-6">
          <button class="btn btn-info btn-sm" @click="followUser">
            follow
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import myAxios from "../plugins/MyAxios";
export default {
  props: ["user"],
  methods: {
    async followUser() {
      let body = { peopleId: this.$props.user.userId };
      let res = await myAxios.post("/api/account/follow", body);
      console.log(res);
      if (res.data) {
        this.$bvToast.toast(`now you follow ${this.$props.user.username} `, {
          title: "done",
          autoHideDelay: 7000,
          appendToast: true,
          variant: "warning",
          solid: true,
        });
      }
    },
  },
};
</script>

<style></style>
