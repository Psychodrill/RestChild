interface JQueryInputMaskOptions {
	mask?: string;
	allowMinus?: boolean;
	rightAlign?: any;
	digits?: any;
	alias?: string;
	placeholder?: string;
	repeat?: number;
	greedy?: boolean;
	skipOptionalPartCharacter?: string;
	clearIncomplete?: boolean;
	clearMaskOnLostFocus?: boolean;
	autoUnmask?: boolean;
	showMaskOnFocus?: boolean;
	showMaskOnHover?: boolean;
	showToolTip?: boolean;
	isComplete?: (buffer, options) => {};
	numeric?: boolean;
	radixPoint?: string;
	min?: number,
	max?: number,
	rightAlignNumerics?: boolean;
	regex?: string;
	oncomplete?: (value?: any) => void;
	onincomplete?: () => void;
	oncleared?: () => void;
	onUnMask?: (maskedValue, unmaskedValue) => void;
	onBeforeMask?: (initialValue) => void;
	onKeyValidation?: (result) => void;
	onBeforePaste?: (pastedValue) => void;
}

interface inputMaskStatic {
	defaults: inputMaskDefaults;
	isValid: (value: string, options: inputMaskStaticDefaults) => boolean;
	format: (value: string, options: inputMaskStaticDefaults) => boolean;
}

interface inputMaskStaticDefaults {
	alias: string;
}

interface inputMaskDefaults {
	aliases;
	definitions;
}

interface JQueryStatic {
	inputmask: inputMaskStatic;
}

interface JQuery {
	inputmask(action: string): any;
	inputmask(mask: string, options?: JQueryInputMaskOptions): JQuery;
	inputmask(options: any): JQuery;
}
