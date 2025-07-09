<template>
  <div class="info-icon" tabindex="0" aria-label="More info">
    ℹ️
    <span class="tooltip-text">
      <slot />
    </span>
  </div>
</template>

<style scoped>
  .info-icon {
    position: relative;
    cursor: default;
    font-size: 1.5rem;
    user-select: none;
    outline: none;
    display: inline-flex;
    align-items: center;
    height: 100%;
  }

  .tooltip-text {
    visibility: hidden;
    width: 200px;
    background-color: #333;
    color: #fff;
    text-align: left;
    border-radius: 4px;
    padding: 0.5rem;
    position: absolute;
    bottom: 125%;
    left: 50%;
    transform: translateX(-50%);
    opacity: 0;
    transition: opacity 0.3s;
    z-index: 10;
    pointer-events: none;
    font-size: small;
  }

  .info-icon:hover .tooltip-text,
  .info-icon:focus .tooltip-text {
    visibility: visible;
    opacity: 1;
    pointer-events: auto;
  }
</style>
