<template>
    <div class="container myFont" v-if="profile.id">
        <div class="row mt-3">
            <div class="col-12">
                <div class="text-center">
                    <img :src="profile.coverImg" class="img-fluid coverImg">
                </div>
                <div class="text-center mUp">
                    <img :src="profile.picture" class="rounded-circle profileImg" alt="">
                    <p class="fs-1 ms-2 fw-semibold"><u>{{ profile.name }}</u></p>
                    <i class="mdi mdi-alpha-k-box-outline fs-3 px-3">{{ keeps.length }}</i>
                    <i class="mdi mdi-safe fs-3 px-3">{{ vaults.length }}</i>
                </div>
            </div>
            <p class="fs-3 fw-bold myFont">Vaults</p>
            <div class="d-flex justify-content-center" v-if="vaults.length == 0">
                <p class="fs-3 fw-light bg-success shrink rounded text-center">{{profile.name}} currently has no Vaults</p>
            </div>
            <div class="row" v-if="vaults.length > 0">
                <div class="col-md-3" v-for="vault in vaults">
                    <VaultCard :vault="vault" />
                </div>
            </div>
            <p class="fs-3 fw-bold myFont">Keeps</p>
            <div class="d-flex justify-content-center" v-if="keeps.length == 0">
                <p class="fs-3 fw-light bg-success shrink rounded text-center">{{profile.name}} currently has no Keeps</p>
            </div>
            <div class="col-12">
                <section class="bricks mx-5">
                    <div v-for="keep in keeps" class="myShadow">
                        <KeepCard :keep="keep" />
                    </div>
                </section>
            </div>
        </div>
    </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState';
import { onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { profileService } from '../services/ProfileService'

export default {
    setup() {

        const route = useRoute();

        async function getProfile() {
            const profileId = route.params.id
            await profileService.getProfile(profileId)
        }

        async function getProfileKeeps() {
            const profileId = route.params.id
            await profileService.getProfileKeeps(profileId)
        }

        async function getProfileVaults() {
            const profileId = route.params.id
            await profileService.getProfileVaults(profileId)
        }

        onMounted(() => {
            getProfile();
            getProfileKeeps();
            getProfileVaults();
        })

        return {
            profile: computed(() => AppState.profile),
            keeps: computed(() => AppState.keeps),
            vaults: computed(() => AppState.vaults)
        }
    }
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
}
</style>