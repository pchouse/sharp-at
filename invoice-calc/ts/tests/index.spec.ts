import {
	LineError,
	LineErrorCause,
	VatRateError,
	VatRateTotalsError,
	Invoice,
	InvoiceLine,
	Stamp,
	VatRate,
	VatRateTotals,
	VatType,
	VatZone,
	InvoiceError
} from '../src';

test('Line Error', () => {
	expect(LineError).toBeDefined();
});

test('Line Error cause', () => {
	expect({
		'cause': 'Line Error cause message',
		'lineNumber': 1n,
		'value': null
	} as LineErrorCause).toBeDefined();
});

test('Vat rate error', () => {
	expect(VatRateError).toBeDefined();
});

test('Vat rate totals error', () => {
	expect(VatRateTotalsError).toBeDefined();
});

test('Invoice error', () => {
	expect(InvoiceError).toBeDefined();
});

test('Invoice', () => {
	expect(Invoice).toBeDefined();
});

test('Invoice line', () => {
	expect(InvoiceLine).toBeDefined();
});

test('Stamp', () => {
	expect(Stamp).toBeDefined();
});

test('Vat rate', () => {
	expect(VatRate).toBeDefined();
});

test('Vat rate totals', () => {
	expect(VatRateTotals).toBeDefined();
});

test('Vat type', () => {
	expect(VatType).toBeDefined();
});

test('Vat zone', () => {
	expect(VatZone).toBeDefined();
});
