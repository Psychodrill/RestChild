function retrocycle(obj: any): void {
	var catalog: any[] = [];
	catalogObject(obj, catalog);
	resolveReferences(obj, catalog);
}

function catalogObject(obj, catalog: any[]): void {

	// The catalogObject function walks recursively through an object graph
	// looking for $id properties. When it finds an object with that property, then
	// it adds it to the catalog under that key.

	var i: number;
	if (obj && typeof obj === 'object') {
		var id: string = obj.$id;
		if (typeof id === 'string') {
			catalog[id] = obj;
		}

		if (Object.prototype.toString.apply(obj) === '[object Array]') {
			for (i = 0; i < obj.length; i += 1) {
				catalogObject(obj[i], catalog);
			}
		} else {
			for (var name in obj) {
				if (typeof obj[name] === 'object') {
					catalogObject(obj[name], catalog);
				}
			}
		}
	}
}

function resolveReferences(obj: any, catalog: any[]) {

	// The resolveReferences function walks recursively through the object looking for $ref
	// properties. When it finds one that has a value that is an id, then it
	// replaces the $ref object with a reference to the object that is found in the catalog under
	// that id.

	var i: number, item: any, name: string, id: string;

	if (obj && typeof obj === 'object') {
		if (Object.prototype.toString.apply(obj) === '[object Array]') {
			for (i = 0; i < obj.length; i += 1) {
				item = obj[i];
				if (item && typeof item === 'object') {
					id = item.$ref;
					if (typeof id === 'string') {
						obj[i] = catalog[id];
					} else {
						resolveReferences(item, catalog);
					}
				}
			}
		} else {
			for (name in obj) {
				if (typeof obj[name] === 'object') {
					item = obj[name];
					if (item) {
						id = item.$ref;
						if (typeof id === 'string') {
							obj[name] = catalog[id];
						} else {
							resolveReferences(item, catalog);
						}
					}
				}
			}
		}
	}
}
