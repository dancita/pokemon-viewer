import { describe, it, expect, vi } from 'vitest';
import { mount } from '@vue/test-utils';
import LoginButton from '@/components/LoginButton.vue';

const loginWithRedirectMock = vi.fn();

vi.mock('@auth0/auth0-vue', () => ({
  useAuth0: () => ({
    loginWithRedirect: loginWithRedirectMock
  })
}));

describe('LoginButton.vue', () => {
  it('renders the login button', () => {
    const wrapper = mount(LoginButton);
    expect(wrapper.get('button').text()).toBe('Login');
  });

  it('calls loginWithRedirect with correct params on click', async () => {
    const wrapper = mount(LoginButton);
    await wrapper.get('button').trigger('click');

    expect(loginWithRedirectMock).toHaveBeenCalledTimes(1);
    expect(loginWithRedirectMock).toHaveBeenCalledWith({
      connection: 'google-oauth2'
    });
  });
});
