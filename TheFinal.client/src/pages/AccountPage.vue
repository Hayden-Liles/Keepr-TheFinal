<template>
  <div class="container myFont" v-if="account.id">
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
      <p class="fs-3 fw-bold myFont">Vaults</p>
      <div class="row" v-if="vaults.length > 0">
        <div class="col-md-3" v-for="vault in vaults">
          <VaultCard :vault="vault" />
        </div>
      </div>
      <p class="fs-3 fw-bold myFont">Keeps</p>
      <div class="col-12">
        <section class="bricks mx-5">
          <div v-for="keep in keeps" class="myShadow">
            <KeepCard :keep="keep" />
          </div>
        </section>
      </div>
    </div>
  </div>
  <KeepModal />
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { accountService } from '../services/AccountService'
import VaultCard from '../components/VaultCard.vue'
export default {
  setup() {
    async function getAccountVaults() {
      try {
        accountService.getAccountVaults();
      }
      catch (error) {
        logger.error(error);
        Pop.error(error);
      }
    }
    async function getAccountKeeps() {
      try {
        accountService.getAccountKeeps();
      }
      catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }
    onMounted(() => {
      getAccountVaults();
      getAccountKeeps();
    });
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.accountVaults),
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { VaultCard }
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

.myShadow {
  box-shadow: 0px 0px 8px 0 rgba(0, 0, 0, 0.5);
  border-radius: 5px;
}

.profileImg {
  height: 100px;
  width: 100px;
}

.coverImg {
  height: 55vh;
  width: 55vw;
}

p {
  margin: 0;
}

.mUp {
  transform: translateY(-50px);
}

.myFont {
  font-family: 'Oxygen', sans-serif;
}

@media (min-width: 768px) {
  .bricks {
    columns: 270px;
    column-gap: $gap;

    &>div {
      margin-top: 20px;
      display: inline-block;
    }
  }
}</style>
