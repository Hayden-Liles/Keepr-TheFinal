<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <CreateKeepModal/>
  <CreateVaultModal/>
  <KeepModal/>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import CreateKeepModal from './components/CreateKeepModal.vue'
import CreateVaultModal from './components/CreateVaultModal.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, CreateKeepModal, CreateVaultModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root{
  --main-height: calc(100vh - 32px - 64px);
  max-width: 100vw !important;
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
