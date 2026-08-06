import { VatRate, VatRateError, VatType, VatZone } from '../../src';
import Decimal                                     from 'decimal.js';

test('Vat rate instance', () => {

  let vatRate = new VatRate(
    VatType.INT, VatZone.PT, new Decimal(9.99), null
  );

  expect(vatRate).toBeInstanceOf(VatRate);
  expect(vatRate.taxExemptionCode).toBeNull();

  vatRate = new VatRate(
    VatType.INT, VatZone.PT, new Decimal(0.0), 'M09'
  );

  expect(vatRate).toBeInstanceOf(VatRate);
  expect(vatRate.taxExemptionCode).toBe('M09');

});

test('Vat rate with tax rate less than zero', () => {

  try {
    const {} = new VatRate(
      VatType.INT, VatZone.PT, new Decimal(-0.01), null
    );

    fail('Vat rate should throw error when tax rate is less than 0');

  } catch (e: any) {
    expect(e).toBeInstanceOf(VatRateError);
    const error = e as VatRateError;
    expect(error.message).toBe('Wrong tax for vat rate');
    expect(error.cause).toBe('Tax less than 0');
  }
});

test('Vat rate with tax rate greater than 100', () => {

  try {

    const {} = new VatRate(
      VatType.INT, VatZone.PT, new Decimal(100.01), null
    );

    fail('Vat rate should throw error when tax rate is greater than 100');

  } catch (e: any) {
    expect(e).toBeInstanceOf(VatRateError);
    const error = e as VatRateError;
    expect(error.message).toBe('Wrong tax for vat rate');
    expect(error.cause).toBe('Tax greater than 100');
  }
});

test('Vat rate with tax equals to zero but tax exemption code not defined', () => {
  try {

    const {} = new VatRate(
      VatType.INT, VatZone.PT, new Decimal(0.0), null
    );

    fail('Vat rate should throw error when tax rate is 0 but tax exemption code not defined');

  } catch (e: any) {
    expect(e).toBeInstanceOf(VatRateError);
    const error = e as VatRateError;
    expect(error.message).toBe('Wrong tax exemption code for vat rate');
    expect(error.cause).toBe('No tax exemption code defined and tax rate is 0');
  }
});

test('Vat rate with tax equals to zero but tax exemption code wrong format', () => {
  try {

    const {} = new VatRate(
      VatType.INT, VatZone.PT, new Decimal(0.0), null
    );

    fail('Vat rate should throw error when tax rate is 0 but tax exemption code not defined');

  } catch (e: any) {
    expect(e).toBeInstanceOf(VatRateError);
    const error = e as VatRateError;
    expect(error.message).toBe('Wrong tax exemption code for vat rate');
    expect(error.cause).toBe('No tax exemption code defined and tax rate is 0');
  }
});


test('Vat type zero with tax vat rate different of zero', () => {
  try {

    const {} = new VatRate(
      VatType.ISE, VatZone.PT, new Decimal(9.99), null
    );

    fail('Vat rate should be zero to vat type ISE');

  } catch (e: any) {
    expect(e).toBeInstanceOf(VatRateError);
    const error = e as VatRateError;
    expect(error.message).toBe('Wrong tax rate for tax type');
    expect(error.cause).toBe('Tax tye is ISE but tax vat rate is not zero');
  }
});
