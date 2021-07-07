import Vue from "vue";
import VueRouter from "vue-router";
import store from "../store";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: () => import(/* webpackChunkName: "Home" */ "../views/Home.vue"),
    meta: { requiresAuth: true },
  },
  {
    path: "/about",
    name: "about",
    component: () =>
      import(/* webpackChunkName: "Home" */ "../views/About.vue"),
    meta: { requiresAuth: true },
  },
  {
    path: "/identity",
    name: "Identity",
    component: () =>
      import(/* webpackChunkName: "Identity" */ "../views/Identity.vue"),
    meta: { requiresAuth: false },
  },
  {
    path: "/detail/:id",
    name: "detail",
    component: () =>
      import(/* webpackChunkName: "detail" */ "../views/JokeDetail.vue"),
    meta: { requiresAuth: true },
  },
  {
    path: "/explorer",
    name: "explorer",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/Explorer.vue"),
    meta: { requiresAuth: true },
  },
];

const router = new VueRouter({
  mode: "history",
  routes,
});

router.beforeEach((to, from, next) => {
  if (store.state.isAuthenticated == false && to.meta.requiresAuth)
    next("identity");
  else next();
});

export default router;
