<template>
  <div class="container-md-fluid pb-3">
    <div class="d-flex justify-content-center" v-if="keeps.length == 0">
      <p class="fs-3 fw-light bg-success shrink rounded text-center mt-3 p-2">There are currently no keeps do you wanna be the first?</p>
    </div>
    <section class="bricks mx-5">
      <div v-for="keep in keeps" class="myShadow">
        <KeepCard :keep="keep" />
      </div>
    </section>
  </div>
  <KeepModal />
</template>

<script>
import KeepCard from '../components/KeepCard.vue';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { onMounted, onUnmounted, watchEffect } from 'vue';
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState';
import CreateKeepModal from '../components/CreateKeepModal.vue';

export default {
  setup() {
    onMounted(() => {
      getAllKeeps();
    });
    onUnmounted(() => {
      AppState.keeps = [];
    });
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      }
      catch (error) {
        logger.error(error);
        Pop.error(error);
      }
    }
    return {
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { CreateKeepModal }
}
</script>

<style scoped lang="scss">
$gap: 2.5em;

.bricks {
  columns: 100px;
  column-gap: .5em;

  &>div {
    margin-top: 20px;
    display: inline-block;
  }
}

@media (min-width: 768px) {
  .bricks {
    columns: 350px;
    column-gap: $gap;

    &>div {
      margin-top: 20px;
      display: inline-block;
    }
  }
}

.myShadow {
  box-shadow: 0px 0px 8px 0 rgba(0, 0, 0, 0.5);
  border-radius: 5px;
}</style>
