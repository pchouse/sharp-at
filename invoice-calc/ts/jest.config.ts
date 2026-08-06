import type { Config } from 'jest';

const config: Config = {
    preset: 'ts-jest',
    testEnvironment: 'node',
    roots: ['<rootDir>/tests'],
    moduleFileExtensions: ['ts', 'js'],
    testMatch: ['**/*.test.ts','**/*.spec.ts'],
    collectCoverageFrom: ['src/**/*.ts'],
    setupFilesAfterEnv: ['jest-expect-message']
};

export default config;
