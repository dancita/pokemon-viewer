import { describe, it, expect, vi } from 'vitest';
import { mount } from '@vue/test-utils';
import LogoutButton from '@/components/LogoutButton.vue';

const logoutMock = vi.fn();

vi.mock('@auth0/auth0-vue', () => ({
  useAuth0: () => ({
    logout: logoutMock
  })
}));

describe('LogoutButton.vue', () => {
  it('renders the logout button', () => {
    const wrapper = mount(LogoutButton);
    const button = wrapper.get('button');
    expect(button.text()).toBe('Logout');
  });

  it('calls logout on click', async () => {
    const wrapper = mount(LogoutButton);
    await wrapper.get('button').trigger('click');
    expect(logoutMock).toHaveBeenCalledTimes(1);
    expect(logoutMock).toHaveBeenCalledWith({
      logoutParams: {
        returnTo: window.location.origin
      }
    });
  });
});
