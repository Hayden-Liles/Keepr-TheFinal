<template>
  <nav class="navbar navbar-expand navbar-dark px-3 pt-4">
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>


    <div class="collapse navbar-collapse justify-content-between pb-1 myBorder" id="navbarText">
      <div class="jusify-self-center d-flex align-items-center ps-3 ps-md-5">
        <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
          <div class="d-flex flex-column align-items-center">
            <img alt="logo" src="../assets/img/Keepr logo.svg" height="50" />
          </div>
        </router-link>
        <div class="myFont d-flex">
          <router-link :to="{ name: 'Home' }" class="btn bg-primary fw-bold text-dark lighten-30 selectable">
            Home
          </router-link>
          <div class="dropdown ps-2" v-if="account.id">
            <button class="btn fw-semibold dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
              Create
            </button>
            <ul class="dropdown-menu text-center">
              <li class="selectable"><button data-bs-toggle="modal" data-bs-target="#CreateKeepModal" class="noBorder w-100 fs-5">New Keep</button></li>
              <li class="selectable"><button data-bs-toggle="modal" data-bs-target="#CreateVaultModal" class="noBorder w-100 fs-5">New Vault</button></li>
            </ul>
          </div>
        </div>
      </div>
      <div class="pe-2 pe-md-5">
        <Login />
      </div>
    </div>
  </nav>
</template>

<script>
import { computed } from '@vue/reactivity';
import Login from './Login.vue'
import { AppState } from '../AppState';
export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.myFont {
  font-family: 'Marko One', serif;
}

.nav-link {
  text-transform: uppercase;
}

.myBorder {
  border-bottom: 2px solid #E9D8D6;
}
.noBorder{
  border: none;
  background-color: transparent;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
    max-width: 100vw;
  }
}

.border-bottom {
  border-bottom: 1px solid var(--primary) !important;
}
</style>
