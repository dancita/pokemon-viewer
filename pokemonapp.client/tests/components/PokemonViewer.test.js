import { describe, it, expect, vi } from 'vitest';
import { mount } from '@vue/test-utils';
import PokemonViewer from '../../src/components/PokemonViewer.vue';

vi.mock('vue-router', () => ({
  useRoute: () => ({ fullPath: '/' }),
  useRouter: () => ({ push: vi.fn() }),
}));

vi.mock('@auth0/auth0-vue', () => {
  return {
    useAuth0: () => ({
      isAuthenticated: { value: true },
      getAccessTokenSilently: vi.fn(() => Promise.resolve('mocked-token')),
    }),
  }
})

describe('PokemonViewer.vue', () => {
  it('mounts without issues', () => {
    const wrapper = mount(PokemonViewer, {
      global: {
        stubs: {
          InfoToolTip: true,
          PokemonDetailsTable: true,
        },
        mocks: {
          $route: { fullPath: '/' },
        }
      }
    });

    expect(wrapper.exists()).toBe(true);
  });

  it('shows error if input is empty', async () => {
    const wrapper = mount(PokemonViewer, {
      global: {
        stubs: {
          InfoToolTip: true,
          PokemonDetailsTable: true,
        },
      },
    });

    const input = wrapper.get('#pokemonQuery');
    await input.setValue('');
    await wrapper.get('[data-testid="submit-button"]').trigger('click');

    const error = wrapper.find('.error-message');
    expect(error.exists()).toBe(true);
    expect(error.text()).toContain('Field cannot be empty.');
  });

  it('shows error if invalid input is entered', async () => {
    const wrapper = mount(PokemonViewer, {
      global: {
        stubs: {
          InfoToolTip: true,
          PokemonDetailsTable: true,
        },
      },
    });

    const input = wrapper.get('#pokemonQuery');
    await input.setValue('pika****'); 
    await wrapper.get('[data-testid="submit-button"]').trigger('click');

    const error = wrapper.find('.error-message');
    expect(error.exists()).toBe(true);
    expect(error.text()).toContain('Only numbers and letters allowed.');
  });

  it('shows error if max length of input is exceeded', async () => {
    const wrapper = mount(PokemonViewer, {
      global: {
        stubs: {
          InfoToolTip: true,
          PokemonDetailsTable: true,
        },
      },
    });

    const input = wrapper.get('#pokemonQuery');
    await input.setValue('pikachuuuuuuuuuuu'); 
    await wrapper.get('[data-testid="submit-button"]').trigger('click');

    const error = wrapper.find('.error-message');
    expect(error.exists()).toBe(true);
    expect(error.text()).toContain('Maximum 12 characters are allowed.');
  });

  it('calls fetchData on valid input', async () => {
    const mockFetch = vi.spyOn(global, 'fetch').mockResolvedValue({
      ok: true,
      json: async () => ({ name: 'Pikachu' }),
    });

    const wrapper = mount(PokemonViewer, {
      global: {
        stubs: { InfoToolTip: true, PokemonDetailsTable: true },
      },
    });

    await wrapper.get('#pokemonQuery').setValue('pikachu');
    await wrapper.get('[data-testid="submit-button"]').trigger('click');

    expect(mockFetch).toHaveBeenCalled();
    mockFetch.mockRestore();
  });

  it('shows PokemonDetailsTable on successful fetchData', async () => {
    const mockPostData = { name: 'Pikachu', id: 25 };

    vi.spyOn(global, 'fetch').mockResolvedValue({
      ok: true,
      json: async () => mockPostData,
    });

    const wrapper = mount(PokemonViewer, {
      global: {
        stubs: {
          InfoToolTip: true,
          PokemonDetailsTable: true,
        },
      },
    });

    const input = wrapper.get('#pokemonQuery');
    await input.setValue('pikachu');
    await wrapper.get('[data-testid="submit-button"]').trigger('click');

    await new Promise(resolve => setTimeout(resolve, 0));

    expect(wrapper.vm.post).toEqual(mockPostData);

    const table = wrapper.findComponent({ name: 'PokemonDetailsTable' });
    expect(table.exists()).toBe(true);

    global.fetch.mockRestore();
  });

  it('shows error message if API call fails', async () => {
    const mockError = { message: 'The requested Pokemon was not found.' };

    vi.spyOn(global, 'fetch').mockResolvedValue({
      ok: false,
      json: async () => mockError,
    });

    const wrapper = mount(PokemonViewer, {
      global: {
        stubs: {
          InfoToolTip: true,
          PokemonDetailsTable: true,
        },
      },
    });

    const input = wrapper.get('#pokemonQuery');
    await input.setValue('pikapika');
    await wrapper.get('[data-testid="submit-button"]').trigger('click');

    await new Promise(resolve => setTimeout(resolve, 0));

    const error = wrapper.find('.error-message');
    expect(error.exists()).toBe(true);
    expect(error.text()).toContain('The requested Pokemon was not found.');

    global.fetch.mockRestore();
  });
})
