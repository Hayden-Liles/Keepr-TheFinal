<template>
    <div class="component">
        <img :src="keep.img" class="img-fluid rounded" alt="">
        <div class="d-flex">
            <p class="px-2 text-light">{{ keep.name }}</p>
            <img :src="keep.creator.picture" class="img-fluid rounded-circle creatorImg" :title="keep.creator.name">
        </div>
    </div>
</template>


<script>
export default {
    props: {
        keep: {
            type: Object,
            required: true
        }
    },
    setup(){
        return {}
    }
}
</script>


<style lang="scss" scoped>
p{
    position: absolute;
    transform: translateY(-20px);
    font-family: 'Marko One', serif;
    font-size: 12px;
    filter: drop-shadow(3px 3px 2px black);
}

.creatorImg{
    position: absolute;
    height: 20px;
    width: 20px;
    filter: drop-shadow(0 0 3px rgb(255, 255, 255));
    transform: translate(10px, -45px);
}

@media (min-width: 768px) {
    .creatorImg{
        height: 50px;
        width: 50px;
        transform: translate(10px, -90px);
    }
    p{
        font-size: 20px;
        transform: translateY(-35px);
    }
}

</style>