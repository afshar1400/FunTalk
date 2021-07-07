<template>
  <div>
    <div class="d-flex justify-content-center my-5" v-if="loading">
      <b-spinner
        variant="warning"
        style="width: 7rem; height: 7rem"
        label="large spinner"
      ></b-spinner>
    </div>
    <div v-else>
      <slot></slot>
    </div>
  </div>
</template>

<script>
export default {
  name: "Loader",
  props: {
    loading: Boolean,
  },
};
</script>

<style></style>
