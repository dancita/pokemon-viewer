import { describe, it, expect } from 'vitest'
import { formatValue, formatHeight, formatWeight, capitalize } from '@/utils/pokemonFormatters'

describe('formatValue', () => {
  it('returns integer if result is whole number', () => {
    expect(formatValue(10, 0.1)).toBe(1)
  })

  it('returns decimal with one digit if needed', () => {
    expect(formatValue(7, 0.1)).toBe('0.7')
    expect(formatValue(15, 0.1)).toBe('1.5')
  })
})

describe('formatHeight', () => {
  it('formats height from decimeters to meters', () => {
    expect(formatHeight(10)).toBe(1)
    expect(formatHeight(7)).toBe('0.7')
  })
})

describe('formatWeight', () => {
  it('formats weight from hectograms to kilograms', () => {
    expect(formatWeight(60)).toBe(6)
    expect(formatWeight(45)).toBe('4.5')
  })
})

describe('capitalize', () => {
  it('capitalizes the first letter', () => {
    expect(capitalize('pikachu')).toBe('Pikachu')
  })

  it('returns empty string for undefined or null', () => {
    expect(capitalize()).toBe('')
    expect(capitalize(null)).toBe('')
  })
})
