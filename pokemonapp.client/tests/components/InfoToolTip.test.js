import { describe, it, expect } from 'vitest';
import { mount } from '@vue/test-utils';
import InfoToolTip from '@/components/InfoToolTip.vue';

describe('InfoToolTip.vue', () => {
  it('renders slot content', () => {
    const wrapper = mount(InfoToolTip, {
      slots: {
        default: 'Helpful tooltip text'
      }
    });

    expect(wrapper.text()).toContain('Helpful tooltip text');
  });
});
