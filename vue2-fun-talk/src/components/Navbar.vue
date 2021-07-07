<template>
  <b-navbar toggleable="lg" type="light" variant="light" class="navbar">
    <div class="container">
      <b-navbar-brand to="/" exact>JokeShare</b-navbar-brand>

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav>
        <!-- Right aligned nav items -->
        <b-navbar-nav class="ml-auto">
          <b-nav-item to="/about" exact
            ><b-icon
              icon="info-circle-fill"
              font-scale="2"
              variant="warning"
            ></b-icon
          ></b-nav-item>
          <b-nav-item to="/" exact
            ><b-icon
              icon="house-door-fill"
              font-scale="2"
              variant="warning"
            ></b-icon
          ></b-nav-item>
          <b-nav-item to="/explorer" exact
            ><b-icon
              icon="compass-fill"
              font-scale="2"
              variant="warning"
            ></b-icon>
          </b-nav-item>

          <b-nav-item-dropdown right>
            <!-- Using 'button-content' slot -->
            <template #button-content>
              <b-avatar variant="warning" text="BV" size="2rem"></b-avatar>
            </template>
            <b-dropdown-item
              ><b-dropdown-item to="/identity" exact>Sign in</b-dropdown-item>
            </b-dropdown-item>
            <b-dropdown-item
              ><b-dropdown-item @click="signOut">Sign out</b-dropdown-item>
            </b-dropdown-item>
          </b-nav-item-dropdown>
        </b-navbar-nav>
      </b-collapse>
    </div>
  </b-navbar>
</template>

<script>
export default {
  methods: {
    signOut() {
      this.$store.commit("signout");
      this.$router.push("/identity");
    },
  },
};
</script>

<style></style>
