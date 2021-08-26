function getScrollbarWidth() {
	var outer = document.createElement("div");
	outer.style.visibility = "hidden";
	outer.style.width = "100px";
	outer.style.msOverflowStyle = "scrollbar"; // needed for WinJS apps

	document.body.appendChild(outer);

	var widthNoScroll = outer.offsetWidth;
	// force scrollbars
	outer.style.overflow = "scroll";

	// add innerdiv
	var inner = document.createElement("div");
	inner.style.width = "100%";
	outer.appendChild(inner);

	var widthWithScroll = inner.offsetWidth;

	// remove divs
	outer.parentNode.removeChild(outer);

	return widthNoScroll - widthWithScroll;
}

function sortByColumn(self, code) {
	var empty = $(self).find('i').hasClass('icon-chevron-down');
	var desceding = $(self).find('i').hasClass('icon-chevron-up');
	if (empty) {
		code = '0';
		desceding = false;
	}

	var mainTrs = $('#table tbody tr');
	var fcTrs = $('#tblFc tbody tr');
	var resMainRows = new Array(mainTrs.length);
	var resFcRows = new Array(fcTrs.length);
	for (var i = 0; i < mainTrs.length; i++) {
		var index = parseInt(mainTrs[i].getAttribute('g' + code));
		if (index < 0) {
			return;
		}

		resMainRows[desceding ? fcTrs.length - index - 1 : index] = mainTrs[i];
	}

	for (var i = 0; i < fcTrs.length; i++) {
		var index = parseInt(fcTrs[i].getAttribute('g' + code));
		if (index < 0) {
			return;
		}
		resFcRows[desceding ? fcTrs.length - index - 1 : index] = fcTrs[i];
	}

	$('#table tbody').append(resMainRows);
	$('#tblFc tbody').append(resFcRows);
	$('i').removeClass('icon-chevron-up');
	$('i').removeClass('icon-chevron-down');

	if (!empty) {
		$(self).find('i').addClass(desceding ? 'icon-chevron-down' : 'icon-chevron-up');
	}
}