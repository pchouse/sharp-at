import { VatRateTotals, VatType, VatZone } from '../../src';
import Decimal                             from 'decimal.js';

test('Vat rate totals instance', () => {

  expect(
    new VatRateTotals(
      VatType.ISE, VatZone.PT_MA, new Decimal(0.0), 'M99', 2n, new Decimal(1.0)
    )
  ).toBeInstanceOf(VatRateTotals);

});

test('Add tax amounts to the vat rate total with tax rate zero', () => {

  const vatRateTotals = new VatRateTotals(
    VatType.ISE, VatZone.PT_MA, new Decimal(0.0), 'M99', 2n, new Decimal(1.0)
  );

  vatRateTotals.addBaseTaxable(new Decimal(9.99));
  expect(vatRateTotals.taxValueAmount.toNumber()).toEqual(Number(0));
  expect(vatRateTotals.baseTaxableAmount.toNumber()).toEqual(Number(9.99));

  vatRateTotals.addBaseTaxable(new Decimal(-9.99));
  expect(vatRateTotals.taxValueAmount.toNumber()).toEqual(Number(0));
  expect(vatRateTotals.baseTaxableAmount.toNumber()).toEqual(Number(0));

  vatRateTotals.addBaseTaxable(new Decimal(-9.99));
  expect(vatRateTotals.taxValueAmount.toNumber()).toEqual(Number(0));
  expect(vatRateTotals.baseTaxableAmount.toNumber()).toEqual(Number(-9.99));

  vatRateTotals.addBaseTaxable(new Decimal(10));
  expect(vatRateTotals.taxValueAmount.toNumber()).toEqual(Number(0));
  expect(vatRateTotals.baseTaxableAmount.toNumber()).toEqual(Number(0.01));

});

test('Add tax amounts to the vat rate total with tax rate greater than zero', () => {

  const vatRateTotals = new VatRateTotals(
    VatType.OUT, VatZone.PT_MA, new Decimal(20.0), null, 2n, new Decimal(1.0)
  );

  vatRateTotals.addBaseTaxable(new Decimal(100.00));
  expect(vatRateTotals.taxValueAmount.toNumber()).toEqual(Number(20.00));
  expect(vatRateTotals.baseTaxableAmount.toNumber()).toEqual(Number(100.00));

  vatRateTotals.addBaseTaxable(new Decimal(-100.00));
  expect(vatRateTotals.taxValueAmount.toNumber()).toEqual(Number(0));
  expect(vatRateTotals.baseTaxableAmount.toNumber()).toEqual(Number(0));

  vatRateTotals.addBaseTaxable(new Decimal(150.00));
  expect(vatRateTotals.taxValueAmount.toNumber()).toEqual(Number(30.00));
  expect(vatRateTotals.baseTaxableAmount.toNumber()).toEqual(Number(150));

});



