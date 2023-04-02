<template>
    <div class="modal" id="KeepModal" tabindex="-1" aria-labelledby="KeepModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-fullscreen-sm-down">
            <div class="modal-content" v-if="keep">
                <div class="row">


                    <div class="col-6 d-flex d-flex">
                        <img :src="keep.img" class="img-fluid keepImg">
                    </div>

                    <div class="col-6 myFont flex-fill d-flex flex-column">
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
                            <div class="text-center p-5">
                                <p class="fs-2 fw-bold">{{ keep.name }}</p>
                                <p>{{ keep.description }}</p>
                            </div>
                        </div>
                        <div class="d-flex flex-grow-1 m-auto mb-3">
                            <div class="d-flex align-items-end">
                                <div class="d-flex justify-content-between">
                                    <button class="me-5 removeBtn" v-if="keep.creator.id == account.id" @click="deleteKeep(keep.id)" data-bs-dismiss="modal">remove</button>
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

export default {
    setup() {
        return {
            keep: computed(() => AppState.activeKeep),
            account: computed(() => AppState.account),

            async deleteKeep(id) {
                if(await Pop.confirm('Are you sure you want to delete this keep?')){
                    await keepsService.deleteKeep(id)
                }else{
                    Pop.toast('Keep not deleted')
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.keepImg {
    height: 70vh;
    width: 100%;
    border-top-left-radius: .5em;
    border-bottom-left-radius: .5em;
}
p{
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
.removeBtn{
    background-color: #f8f9fa;
    border: none;
    border-bottom: 1px solid #6c757d;
    color: #6c757d;
    font-size: 1.5em;
    font-weight: 600;
}
</style>