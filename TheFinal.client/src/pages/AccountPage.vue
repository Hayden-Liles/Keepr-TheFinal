<template>
  <div class="container">
    <div class="row mt-3">
      <div class="col-12">
        <div class="d-flex align-items-center">
          <img :src="account.picture" class="rounded-circle" alt="">
          <p class="fs-1 ms-2 fw-semibold"><u>{{ account.name }}</u></p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
p{
  margin: 0;
}
</style>
