<template>
    <div class="modal" id="KeepModal" tabindex="-1" aria-labelledby="KeepModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-fullscreen-sm-down">
            <div class="modal-content" v-if="keep">
                <div class="row">


                    <div class="col-12 col-md-6 order-2 order-md-1 d-flex d-flex">
                        <img :src="keep.img" class="img-fluid keepImg">
                    </div>

                    <div class="col-12 col-md-6 myFont order-1 order-md-2 flex-fill d-flex flex-column">
                        <div class="d-flex justify-content-between mt-2 mb-5">
                            <div class="">
                                <i class="mdi mdi-eye-outline fs-3 px-3">{{ keep.views }}</i>
                                <i class="mdi mdi-alpha-k-box-outline fs-3 px-3">{{ keep.kept }}</i>
                            </div>
                            <div>
                                <button type="button" class="mdi mdi-close mbSize" data-bs-dismiss="modal"
                                    aria-label="Close"></button>
                            </div>
                        </div>
                        <div class="">
                            <div class="text-center p-1 p-md-5 ofScroll">
                                <p class="fs-2 fw-bold">{{ keep.name }}</p>
                                <p>{{ keep.description }}</p>
                            </div>
                        </div>
                        <div class="d-flex flex-grow-1 m-auto mb-3">
                            <div class="d-flex align-items-end">
                                <div class="d-md-flex text-center text-md-start justify-content-between">
                                    <button class="me-1 removeBtn"
                                        v-if="keep.creator.id == account.id && route.name != 'Vaults'"
                                        @click="deleteKeep(keep.id)" data-bs-dismiss="modal">
                                        delete
                                    </button>
                                    <button class="me-1 removeBtn"
                                        v-if="route.name == 'Vaults' && curVault.creator.id == account.id"
                                        @click="removeKeepFromVault(keep.id)" data-bs-dismiss="modal">
                                        remove
                                    </button>
                                    <div class="d-flex align-items-center me-md-5 my-3" v-if="account.id">
                                        <form @submit.prevent="addKeepToVault(editable.id, keep.id)" class="d-flex">
                                            <div class="form-group">
                                                <select class="form-select noBorder" aria-label="Default select example"
                                                    v-model="editable.id">
                                                    <option v-for="vault in accVault" :value="vault.id">{{ vault.name }}
                                                    </option>
                                                </select>
                                                <p class="formDefault" v-if="!editable.id">Save to a vault</p>
                                            </div>
                                            <button class="btn btn-sm bg-success">save</button>
                                        </form>
                                    </div>
                                    <div class="d-flex align-items-end">
                                        <img :src="keep.creator.picture" class="creatorImg me-2">
                                        <p>{{ keep.creator.name }}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import Pop from '../utils/Pop';
import { accountService } from '../services/AccountService';
import { onMounted, ref } from 'vue';
import { logger } from '../utils/Logger';
import { useRoute } from 'vue-router';

export default {
    setup() {
        const route = useRoute()
        const editable = ref({})
        async function getUsersVaults() {
            await accountService.getAccountVaults()
        }

        onMounted(() => {
            getUsersVaults()
            logger.log(route.name)
        })

        return {
            keep: computed(() => AppState.activeKeep),
            curVault: computed(() => AppState.activeVault),
            account: computed(() => AppState.account),
            accVault: computed(() => AppState.accountVaults),
            editable,
            route,

            async deleteKeep(id) {
                if (await Pop.confirm('Are you sure you want to delete this keep?')) {
                    await keepsService.deleteKeep(id)
                } else {
                    Pop.toast('Keep not deleted')
                }
            },

            async addKeepToVault(vaultId, keepId) {
                try {
                    editable.value = {}
                    await keepsService.addKeepToVault(vaultId, keepId)
                    Pop.toast('Keep added to vault')
                } catch (error) {
                    logger.log(error)
                }
            },

            async removeKeepFromVault(keepId) {
                try {
                    if (await Pop.confirm('Are you sure you want to delete this keep?')) {
                        await keepsService.removeKeepFromVault(keepId)
                        Pop.toast('Keep removed from vault')
                    } else {
                        Pop.toast('Keep was not removed from vault')
                    }

                } catch (error) {
                    logger.log(error)
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.formDefault {
    position: absolute;
    transform: translate(15px, -30px);
}

.noBorder {
    border: none !important;
    background-color: transparent;
}

.keepImg {
    height: 100vh;
    width: 100%;
    border-top-left-radius: .5em;
    border-bottom-left-radius: .5em;
}

.saveBtn {
    height: 30px;
}

.mySelect {
    max-height: 50vh;
    overflow-y: scroll;
}

.ofScroll {
    overflow-y: scroll;
}

p {
    margin: 0;
}

.creatorImg {
    height: 3em;
    width: 3em;
    border-radius: 50%;
}

.modal {
    --bs-modal-width: 60vw;
}

.mbSize {
    font-size: 2em;
    border: none;
}

.myFont {
    font-family: 'Oxygen', sans-serif;
}

.removeBtn {
    background-color: #f8f9fa;
    border: none;
    border-bottom: 1px solid #6c757d;
    color: #6c757d;
    font-size: 1.5em;
    font-weight: 600;
}

@media (min-width: 768px) {
    .keepImg {
        height: 100%;
        max-height: 80vh;
        width: 100%;
        border-top-left-radius: .5em;
        border-bottom-left-radius: .5em;
    }
}</style>