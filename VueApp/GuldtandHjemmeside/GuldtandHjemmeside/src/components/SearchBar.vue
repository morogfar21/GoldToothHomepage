<template>
    <div class="body">
        <div class="SearchBar">
            <!-- <typeahead :suggestions="searchoptions" :selection.sync="value"> </typeahead>  -->
            <input type="text" v-model="searchParameter" placeholder="Indtast navnet af en opskrift eller en ingrediens" required>
            <!--<autocomplete :items="[ 'Apple', 'Banana', 'Orange', 'Mango', 'Pear', 'Peach', 'Grape', 'Tangerine', 'Pineapple']" />

        <script type="text/x-template" id="autocomplete">
            <div class="autocomplete">
                <input type="text" @input="onChange" v-model="search" @keyup.down="onArrowDown" @keyup.up="onArrowUp" @keyup.enter="onEnter" />
                <ul id="autocomplete-results" v-show="isOpen" class="autocomplete-results">
                    <li class="loading" v-if="isLoading">
                        Loading results...
                    </li>
                    <li v-else v-for="(result, i) in results" :key="i" @click="setResult(result)" class="autocomplete-result" :class="{ 'is-active': i === arrowCounter }">
                        { result }}
                    </li>
                </ul>

            </div>
        </script>-->

        </div>

        <div class="SearchBarBtn">
            <button class="SearchBtn" @click="searchRecepie" type="button">S&#248;g opskrift</button>
            <!--<button style="height: 32px;" @click="$emit('triggerEvent')" type="button">S&#248;g ingrediens</button> -->
        </div>

        <br style="clear:both" />

        <span v-html="info2">{{info2}}</span>
    </div>
</template>

<script>

    //import Typeahead from './components/Typeahead.vue'
    export default {
        name: 'SearchBar',
        //components: { Typeahead },

        props: {
            //['searchoptions', 'value']


            //searchoptions: ['k�d', 'gr�nsager', 'agurk'],
            //value: ''
        },



        data: function () {
            return {
                test: 'Det virker',
                info: null,
                info2: null,
                searchParameter: null,
                relevantStores: ""
                //searchoptions: ['k�d', 'gr�nsager', 'agurk'],
                //value: ''
            }
            /* {
                 searchoptions: [
                     'k�d', 'gr�nsager', 'agurk', 'D', 'K', 'M'
                 ],
                 value: ''
             }*/
        },
        methods: {
            searchRecepie() {
                this.$http.get('https://nyguldtand.azurewebsites.net/Recipe/ViewForSmallRecipeSearch?word=' + this.searchParameter + '&stores=' + this.relevantStores, {
                    headers: {
                        'Access-Control-Allow-Origin': '*',
                    },
                }).then(response => (this.info2 = response.data))

            }
        },
        beforeMount() {
            this.$root.$on('clickedSaveStores', (stores) => {
                this.relevantStores = stores;
            })
        },
    }


</script>

<style scoped>
    .SearchBar {
        width: 65%;
        display: block;
        position: relative;
        float: left;
        color: black;
        font-size: 24px;
        text-align: left;
        margin-right: 30px;
        margin-top: 7px;
    }

        .SearchBar input {
            width: 100%;
            padding: 10px;
        }

    .SearchBtn {
        width: 14%;
        height: 42px;
        display: block;
        position: relative;
        float: left;
        color: black;
        font-size: 16px;
        float: left;
        margin: 6px;
    }
</style>
