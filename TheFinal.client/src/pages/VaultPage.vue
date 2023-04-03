<template>
    <div class="container myFont" v-if="vault && (!vault.isPrivate || vault.creator.id == account?.id)">
        <div class="row mt-4">
            <div class="col-md-5 m-auto text-center">
                <img :src="vault.img" class="vaultImg"> 
                <div class="text-light">
                    <p class="fs-5 fw-semibold myP">By {{ vault.creator.name }}</p>
                    <p class="fs-3 fw-bold p-4 myP">{{ vault.name }}</p>
                </div>
                <div class="text-end fs-5 p-0 m-0">
                    <div class="dropdown ps-2" v-if="account.id">
                        <button class="btn fw-semibold dropdown-toggle mdi mdi-dots-horizontal" type="button" data-bs-toggle="dropdown"
                            aria-expanded="false">
                        </button>
                        <ul class="dropdown-menu text-center">
                            <li><button class="btn selectable w-100" @click="DeleteVault(vault.id)">Delete Vault</button></li>
                        </ul>
                    </div>
                </div>
                <div class="d-flex justify-content-center">
                    <p class="fs-5 fw-semibold keepsP px-2 py-1 rounded bg-success">{{ keeps.length }} Keeps</p>
                </div>
            </div>
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
import { useRoute, useRouter } from 'vue-router';
import { vaultsService } from '../services/VaultsService';
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState';
import { onMounted } from 'vue';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {
    setup() {
        const route = useRoute();
        const router = useRouter();
        const routeId = route.params.id;

        async function getVaultById() {
            try {
                const check = vaultsService.getVaultById(routeId);
                if(check == 'goBack'){
                    logger.log("hi")
                    router.push({ name: "Home" })
                }
            }
            catch (error) {
                router.push({ name: "Home" })
                logger.error(error)
                Pop.error(error)
            }
        }

        onMounted(() => {
            getVaultById();
            setTimeout(() => {
            if(AppState.activeVault == null || AppState.activeVault.isPrivate && AppState.activeVault.creator.id != AppState.account.id){
                Pop.error("The Vault You tried to view is private")
                router.push({ name: "Home" })
            }
        }, 1000);
        })
        return {
            vault: computed(() => AppState.activeVault),
            keeps: computed(() => AppState.keeps),
            account: computed(() => AppState.account),

            async DeleteVault(id){
                try {
                    if(await Pop.confirm("Are you sure you want to delete this vault?")){
                        await vaultsService.DeleteVault(id)
                        Pop.toast("Vault Deleted")
                        router.push({ name: "Account" })
                    }
                    else{
                        Pop.toast("Vault Not Deleted")
                    }
                }
                catch(error) {
                    logger.error(error)
                    Pop.error(error)
                }
            },
        }
    }
}
</script>


<style lang="scss" scoped>
.vaultImg {
    height: 400px;
    width: 100%;
}

p {
    margin: 0;
}

.myP {
    position: absolute;
    transform: translate(20px, -70px);
    font-family: 'Marko One', serif;
    filter: drop-shadow(3px 3px 2px rgb(0, 0, 0));
}

.keepsP {
    width: fit-content;
}

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
}
</style>