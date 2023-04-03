<template>
    <router-link :to="{ name: 'Vaults', params: {id: vault.id} }">
        <div class="mb-5">
            <img :src="vault.img" class="image-fluid">
            <p class="fs-4 fw-bold myFont ps-3 text-light">{{ vault.name }}</p>
        </div>
    </router-link>
</template>


<script>
import { RouterLink } from 'vue-router';

export default {
    props: {
        vault: {
            type: Object,
            required: true
        }
    },
    setup() {
        return {};
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

p{
    margin: 0;
    position: absolute;
    transform: translateY(-40px);
    font-family: 'Marko One', serif;
    font-size: 12px;
    filter: drop-shadow(3px 3px 2px rgb(0, 0, 0));
}
</style>