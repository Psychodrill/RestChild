$(() => {
    const template = $(".OrphanageAddress").first().get(0).outerHTML;

    setStreetAR($(".street-autocompleteAR").not("inited").addClass("inited"));
    InitSelects();

    $(".OrphanageFormSave").click((e) => {
        $(e.target).prop("disabled", true);
        $("#OrphanageForm").submit();
    });

    $(".address-add").click(() => {
        const OrphanageAddressIds = $(".OrphanageAddressIndex").map(function() { return $(this).val(); }).get();
        const newOrphanageAddressId = Math.max.apply(Math, OrphanageAddressIds) + 1;
        const block = $(template);
        block.insertAfter($(".OrphanageAddress").last());
        clearBlock(block);
        renumBlock(block, newOrphanageAddressId);
        setStreetAR(block.find(".street-autocompleteAR").not("inited").addClass("inited"));
    });

    $("#OrphanageForm").on("click",
        ".address-remove",
        () => {
            var OrphanageAddressIds = $(".OrphanageAddressId").map(function() { return $(this).val(); }).get();
            var block = $(event.target).closest(".OrphanageAddress");
            if (OrphanageAddressIds.length > 1) {
                block.remove();
            } else {
                clearBlock(block);
            }
        });

    function clearBlock(Block) {
        Block.find(":input").each(function() {
            if ($(this).is(":checkbox")) {
                $(this).prop("checked", false);
            }
            if ($(this).is(".forClear")) {
                $(this).val("");
            }
        });
        Block.find("input.street-autocompleteAR.forClear").attr("data-default-id", "").attr("data-default-text", "")
            .attr("data-default-district", "").attr("data-default-region", "");
        Block.find(".select2-chosen").html("");
    }

    function renumBlock(block, newNumber) {
        block.find(".OrphanageAddressIndex").first().val(newNumber);
        block.find(".OrphanageAddressId").first().val(0);
        block.find(".AddressId").first().val(0);
        block.find(":input").each(function() {
            const name = $(this).attr("name");
            const id = $(this).attr("id");
            if ($(this).attr("name") !== undefined) {
                $(this).attr("name",
                    $(this).attr("name").replace(/OrphanageAddress\[(.*?)\]/g, `OrphanageAddress[${newNumber}]`));
            }
            if ($(this).attr("id") !== undefined) {
                $(this).attr("id",
                    $(this).attr("id").replace(/OrphanageAddress\_(.*?)\_{2}/g, `OrphanageAddress_${newNumber}__`));
            }
        });
    }

    function setStreetAR(element) {
        element.select2({
                initSelection: function(element, callback) {
                    const data = {
                        id: element.attr("data-default-id"),
                        text: element.attr("data-default-text"),
                        district: element.attr("data-default-district"),
                        region: element.attr("data-default-region")
                    };
                    callback(data);
                },
                minimumInputLength: 3,
                formatResult: function(data, container, query) {
                    if (data.district) {
                        $(container).attr("data-default-district", data.district);
                    }
                    if (data.region) {
                        $(container).attr("data-default-region", data.region);
                    }
                    return data.text;
                },
                ajax: {
                    url: "/api/WebFIAS/SearchHome",
                    quietMillis: 250,
                    type: "GET",
                    data: function(term, page) {
                        return { Query: term };
                    },
                    results: function(data, page) {
                        const result = [];
                        for (let i = 0; i < data.suggestions.length; i++) {
                            if (data.suggestions[i].data.fias_id) {
                                result.push({
                                    id: data.suggestions[i].data.fias_id,
                                    text: data.suggestions[i].value,
                                    district: data.suggestions[i].data.district,
                                    region: data.suggestions[i].data.adm_area
                                });
                            }
                        }
                        return {
                            results: result
                        };
                    },
                    cache: true
                }
            })
            .on("change",
                function(e) {
                    const block = $(this).closest(".OrphanageAddress");
                    //block.find(".OrphanageAddressDistrict").val(e.added.district);
                    //block.find(".OrphanageAddressRegion").val(e.added.region);
                    block.find(".OrphanageAddressName").val(e.added.text);
                });
    }

});

function InitSelects() {
    $("select").select2();
}
