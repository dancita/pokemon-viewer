import { mount } from '@vue/test-utils'
import PokemonDetailsTable from '../../src/components/PokemonDetailsTable.vue'

describe('PokemonDetailsTable', () => {
  const mockPost = {
    name: 'pikachu',
    height: 4,
    weight: 60,
    sprites: {
      frontDefaultImage: 'https://example.com/pikachu.png'
    },
    types: [
      {
        slot: 1,
        type: {
          name: 'electric',
          url: 'https://pokeapi.co/api/v2/type/13/'
        }
      }
    ],
    abilities: [
      {
        is_hidden: false,
        slot: 1,
        ability: {
          name: 'static',
          url: 'https://pokeapi.co/api/v2/ability/9/'
        }
      }
    ]
  }

  it('renders name, image, height, and weight correctly', () => {
    const wrapper = mount(PokemonDetailsTable, {
      props: { post: mockPost }
    })

    expect(wrapper.text()).toContain('Name')
    expect(wrapper.text()).toContain('Pikachu')
    expect(wrapper.text()).toContain('Height')
    expect(wrapper.text()).toContain('0.4 m')
    expect(wrapper.text()).toContain('Weight')
    expect(wrapper.text()).toContain('6 kg')

    const img = wrapper.find('img')
    expect(img.exists()).toBe(true)
    expect(img.attributes('src')).toBe(mockPost.sprites.frontDefaultImage)
  })

  it('renders types correctly', () => {
    const wrapper = mount(PokemonDetailsTable, {
      props: { post: mockPost }
    })

    expect(wrapper.text()).toContain('Slot: 1')
    expect(wrapper.text()).toContain('Name: electric')
  })

  it('renders abilities correctly', () => {
    const wrapper = mount(PokemonDetailsTable, {
      props: { post: mockPost }
    })

    expect(wrapper.text()).toContain('IsHidden: No')
    expect(wrapper.text()).toContain('Ability: static')
    expect(wrapper.html()).toContain('href="https://pokeapi.co/api/v2/ability/9/"')
  })
})
