<template>
  <div class="container">
    <div class="row mt-3">
      <div class="col-12">
        <div class="text-center">
          <img :src="account.coverImg" class="img-fluid coverImg">
        </div>
        <div class="text-center mUp">
          <img :src="account.picture" class="rounded-circle profileImg" alt="">
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
.profileImg{
  height: 100px;
  width: 100px;
}
.coverImg{
  height: 50vh;
  width: 50vw;
}
p{
  margin: 0;
}
.mUp{
  transform: translateY(-50px);
}
</style>
