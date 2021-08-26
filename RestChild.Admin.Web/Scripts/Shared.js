function newGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, newGUID_v = c == 'x' ? r : (r & 0x3 | 0x8);
        return newGUID_v.toString(16);
    });
}
;
//# sourceMappingURL=Shared.js.map