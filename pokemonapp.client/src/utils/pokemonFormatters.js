export function formatValue(value, multiplier) {
  const result = value * multiplier;
  return Number.isInteger(result) ? result : result.toFixed(1);
}

export function formatHeight(decimeters) {
  return formatValue(decimeters, 0.1);
}

export function formatWeight(hectograms) {
  return formatValue(hectograms, 0.1);
}

export function capitalize(name) {
  if (!name) return '';
  return name.charAt(0).toUpperCase() + name.slice(1);
}
