<template>
    <router-link :to="{ name: 'Vaults', params: {id: vault.id} }">
        <div class="mb-5" v-if="!vault.isPrivate || vault.creator.id == account.id">
            <img :src="vault.img" class="image-fluid">
            <div class="moveUp d-flex">
                <p class="fs-4 fw-bold myFont ps-3 text-light">{{ vault.name }}</p>
                <i v-if="vault.isPrivate" class="mdi mdi-lock fs-5"></i>
            </div>
        </div>
    </router-link>
</template>


<script>
import { computed } from '@vue/reactivity';
import { RouterLink } from 'vue-router';
import { AppState } from '../AppState';
import { logger } from '../utils/Logger';

export default {
    props: {
        vault: {
            type: Object,
            required: true
        }
    },

    setup() {
        return {
            account: computed(() => AppState.account)
        };
    },
    components: { RouterLink }
}
</script>


<style lang="scss" scoped>
img {
    height: 200px;
    width: 100%;
}

.myFont {
    font-family: 'Oxygen';
}

.moveUp{
    margin: 0;
    position: absolute;
    transform: translateY(-40px);
    font-family: 'Marko One', serif;
    font-size: 12px;
    filter: drop-shadow(3px 3px 2px rgb(0, 0, 0));
}
</style>